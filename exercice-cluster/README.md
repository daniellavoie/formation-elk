# Exercice - Intégration de donnée dans un Cluster

A partir de 3 noeuds, former un Cluster


## Configurations importantes

cluster.name

discovery.zen.ping.unicast.hosts

## Intégration des données

* Détruire l'index `address` s'il est déjà présent.
* Récré
* Préparer 3 instances logstash.
* Diviser les fichiers de donnée brute 

## Suppression de l'index

```
DELETE address
```

## Création d'un mapping adapté à address

```
PUT address
{
    "mappings": {
      "v1" :{
        "properties": {
          "nom_voie": {
            "type": "string",
            "analyzer": "french"
          },
          "id_fantoir": {
            "type": "string",
            "index": "not_analyzed"
          },
          "numero": {
            "type": "string",
            "index": "not_analyzed"
          },
          "rep": {
            "type": "string",
            "index": "not_analyzed"
          },
          "code_insee" : {
            "type": "string",
            "index": "not_analyzed"
          },
          "code_post": {
            "type": "string",
            "index": "not_analyzed"
          },
          "alias": {
            "type": "string",
            "analyzer": "french"
          },
          "nom_ld": {
            "type": "string",
            "index": "not_analyzed"
          },
          "nom_afnor":{
            "type": "string",
            "index": "not_analyzed"
          },
          "libelle_acheminement":{
            "type": "string",
            "analyzer": "french"
          },
          "x" : {
             "type": "float"
          },
          "y": {
            "type": "float"
          },
          "location":{
            "type": "geo_point"
          },
          "nom_commune":{
            "type": "string",
            "analyzer": "french"
          }
        }
      }
    }
}
```

## Configuration Logstash

```
input {
  file {
    codec => plain{
      charset => "UTF-8"
    }
    path => ["/Users/daniellavoie/Projects/formation-elk/data/test-data/*.csv"]
    sincedb_path => "/Users/daniellavoie/Projects/formation-elk/data/test-data/.sincedb_path"
    start_position => "beginning"
  }
}

filter {
  csv {
    columns => ["id", "nom_voie", "d_fantoir", "numero", "rep", "code_insee", "code_post", "alias", "nom_ld", "nom_afnor", "libelle_acheminement", "x", "y", "lon", "lat", "nom_commune"]
    separator => ";"
    source => message
  }

  if [x] == "x" {
    drop { }
  }

  if [lon] and [lat] {
    mutate {
      add_field => {
        "[location][lat]" => "%{lat}"
        "[location][lon]" => "%{lon}"
      }
      convert => {
        "[location][lat]" => "float"
        "[location][lon]" => "float"
      }
    }
    mutate {
      remove_field => [ "message", "path", "host", "lon", "lat" ]
    }
  }
}

output {
  elasticsearch {
    hosts => ["localhost:9200"]
    index => "address"
    document_type => "v1"
    document_id => "%{id}"
  }
}
```
