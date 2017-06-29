# Import test data

### Unzip test-data.zip to the location of your choice.

### Create the data mapping.

Before inserting test data, we provide Elasticsearch instruction on how to index the data we are about to send him.

Do do so, reach out Kopf's Rest console from http://localhost:9200/_plugin/kopf/#!/rest.

Then type the `/address` http resource into the path input text box. Set the request method to `PUT`. Finally, you can copy the following json mapping into the http body text area :

```
PUT address
{
  "mappings": {
    "v1": {
      "properties": {
        "nom_voie": {
          "type": "text",
          "analyzer": "french"
        },
        "id_fantoir": {
          "type": "keyword"
        },
        "numero": {
          "type": "keyword"
        },
        "rep": {
          "type": "keyword"
        },
        "code_insee": {
          "type": "keyword"
        },
        "code_post": {
          "type": "keyword"
        },
        "alias": {
          "type": "text",
          "analyzer": "french"
        },
        "nom_ld": {
          "type": "keyword"
        },
        "nom_afnor": {
          "type": "keyword"
        },
        "libelle_acheminement": {
          "type": "text",
          "analyzer": "french"
        },
        "location": {
          "type": "geo_point"
        },
        "nom_commune": {
          "type": "text",
          "analyzer": "french"
        }
      }
    }
  }
}
```

You are now ready to execute the HTTP request by clicking on `Send`. Expected result should be the following :

```
{
  "acknowledged": true
}
```

You have now given instruction to Elasticsearch on how you should handle the fields we will index into our cluster.

### Download logstash

https://www.elastic.co/downloads/logstash

### Unzip to a distribution folder of your choice

### Prepare the import configuration file for logstash.

Create a file at the location of your choice. Update the `%PATH_TO_DATA_FOLDER%`
with the location of the data files you downloaded.

```
input {
  file {
    codec => plain{
      charset => "UTF-8"
    }
    path => ["%PATH_TO_DATA_FOLDER%/*.csv"]
    sincedb_path => "%PATH_TO_DATA_FOLDER%/.sincedb_path"
    start_position => "beginning"
  }
}

filter {
  csv {
    columns => ["id", "nom_voie", "d_fantoir", "numero", "rep", "code_insee", "code_post", "alias", "nom_ld", "nom_afnor", "libelle_acheminement", "x", "y", "lon", "lat", "nom_commune"]
    separator => ";"
    source => message
  }

  if [lon] and [lat] {
    mutate {
      add_field => {
        "[location][lat]" => "%{lat}"
        "[location][lon]" => "%{lon}"
      }
    }
    mutate {
      convert => {
        "x" => "float"
        "y" => "float"
        "[location][lat]" => "float"
        "[location][lon]" => "float"
      }
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

### Launch logstash.

Launch the following command by replacing `%PATH_TO_IMPORT_CONF%` by the path
to your logstash configuration file.

```
$ logstash -f $ %PATH_TO_IMPORT_CONF_FILE%
```
