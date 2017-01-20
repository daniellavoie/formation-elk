# Indexation de tweets en temps réel

## Etapes de l'exercices

* Créer un compte Twitter
* Identifier les champs à indexer
* Installer le plugin prune pour logstash
* Tester une configuration logstash qui consomme des tweets
* Filtrer les champs inutiles des les tweets
* Préparer un mapping Elasticsearch
* Configurer Logstash pour alimenter Elasticsearch

## Créer un compte Twitter

## Identifier les champs à indexer

Description des champs disponible ici : https://dev.twitter.com/overview/api/tweets

### Champs intéressants

* text
* coordinates
* timestamp_ms

## Installer le plugin prune pour Logstash

```
logstash-plugin install logstash-filter-prune
```

## Tester une configuration logstash qui consomme des tweets

### Sortie fichier

Le `output` plugin `file` permettra de valider la structure des messages en sortie de logstash.

https://www.elastic.co/guide/en/logstash/current/plugins-outputs-file.html

### Gestion de la date du tweet

Les tweets comportent un champs `timestamp_ms` qui déclare en millisecondes la date de création du tweet. 
Le filter plugin `date` peut donc être utilisé pour mapper cette valeur sur le champs @timestamp généré par Logstash.

Petite astuce, le format `UNIX_MS` de la fonction `match` permettra de gérer correctement la date du tweet.

https://www.elastic.co/guide/en/logstash/current/plugins-filters-date.html

### Gestion des données de Géolocalisation

Pour stocker un `geo_point`, Elastisearch s'attend à ce que la donnée inséré au formation JSON soit structuré de cette manière :

``` 
  ...
  "champ_geo" : {
    "lon" : 121.122,
    "lat" : 121.241
  }
```

La donnée de Twitter est plûtot structuré de cette manière : 

```
  "coordinates": {
    "coordinates": [-73.981572,40.769137],
    "type" : "Point"
  }
```

Par conséquent, un filter plugin `mutate` doit être configuré pour formatter correctement la donnée. Exemple :

```
   mutate {
      add_field => {
        "[location][lat]" => "%{[coordinates][coordinates][0]}"
        "[location][lon]" => "%{[coordinates][coordinates][1]}"
      }
    }
```

## Filtrer les champs inutiles des les tweets

### Ignorer les tweets sans donnée de geolocalisation

La filter fonction `drop` permet de filtrer des messages entier lorsqu'on l'utilisation avec une condition.
```
# Filtrage des tweets sans coordonnées
  if ![coordinates]{
    drop { }
  }
```

### Plugin Prune

`prune` est un plugin qui permet d'exclure tous les champs d'un message logstash pour ne garder que ceux nous intéresse.

Le paramètre `whitelist_names` permet de déclarer les champs à ne pas filtrer.

https://www.elastic.co/guide/en/logstash/current/plugins-filters-prune.html

## Préparer un mapping Elasticsearch

Le nouvel index qui doit accueillir les tweets doit au préalable avoir un mapping adapté aux champs que nous comptons indexer.

###  Quelques suggestions de mapping pour les champs

#### Champ @timestamp

```
  "@timestamp" : {
    "type": "date"
  }
```

#### Champ location

```
  "location":{
    "type": "geo_point"
  }
```

## Configurer Logstash pour alimenter Elasticsearch

## Configuration Logstash

```
input {
	twitter {
      consumer_key => "lCJFKOXymi0Z4lr4yYuNBrpSX"
      consumer_secret => "PYgfHS0D2bKJQmpn3IGkQAdO9tyk7IGxHwbT0jMQFtythTSdDa"
      oauth_token => "255309054-d1X1lhUNPionZ5xGghkdLgVlRp1RJz19xyXOUPyB"
      oauth_token_secret => "g6ILV8HRdpkABLaJ4E16kBiDhyznYLhCVpUKR0vOd9zTl"
      locations => "-122.75,36.8,-121.75,37.8"
      full_tweet => true
  }
}

filter {
	if ![coordinates] {
		# Retourne false
		drop { }
	}

	date {
		match => ["timestamp_ms", "UNIX_MS"]
	}

	mutate {
      add_field => {
        "[location][lat]" => "%{[coordinates][coordinates][0]}"
        "[location][lon]" => "%{[coordinates][coordinates][1]}"
      }
    }

    prune {
    	whitelist_names => [ "text", "location" , "@timestamp" ]
    }
}

output {
	file {
  		path => "/Users/daniellavoie/Projects/formation-elk/output-logstash/twitter-outpot.json"
	}
}
```

## Mapping Elasticsearch

```
PUT sf-tweets
{
  "mappings": {
    "v1" : {
      "properties": {
        "@timestamp" : {
          "type" : "date"
        },
        "location" : {
          "type": "geo_point"
        }
      }
    }
  }
}
``` 
