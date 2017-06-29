# Installation du plugin AWS S3

```
bin/elasticsearch-plugin install repository-s3
```

# Configuration des credentials AWS parmis les configuration Elasticsearch

## Configuration par elasticsearch.yml
```
cloud:
    aws:
        access_key: AKVAIQBF2RECL7FJWGJQ
        secret_key: vExyMThREXeRMm/b/LRzEB8jWwvzQeXgjqMX+6br
```

## Configuration par ligne de commande
```
./elasticsearch -Ecloud.aws.access_key=AKVAIQBF2RECL7FJWGJQ -Ecloud.aws.secret_key=vExyMThREXeRMm/b/LRzEB8jWwvzQeXgjqMX+6br
```

# Création d'un repository de Snapshot

```
PUT _snapshot/aws
{
  "type": "s3",
  "settings": {
    "bucket": "es-curator-bgl",
    "region": "eu-west-1"
  }
}
```

# Création d'un snapshot complet

```
PUT /_snapshot/aws/snapshot-name?wait_for_completion=true
```

# Création d'un snapshot sélectif

```
PUT /_snapshot/aws/snapshot-name?wait_for_completion=true
{
  "indices": "index_1,index_2",
  "ignore_unavailable": true,
  "include_global_state": false
}
```
