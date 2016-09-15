# Download and install Elasticsearch

### Download and install Java 8

JDK 8 : https://www.java.com/fr/download/

### Add Java to your PATH

Test with unix :

```
$ java -version
openjdk version "1.8.0_91"
OpenJDK Runtime Environment (build 1.8.0_91-8u91-b14-1~bpo8+1-b14)
OpenJDK 64-Bit Server VM (build 25.91-b14, mixed mode)
```

Test with Windows

```
TODO
```

### Download Elasticsearch

https://www.elastic.co/downloads/elasticsearch

### Unzip to a distribution folder

### Start Elasticsearch

Windows :
```
bin/elastisearch.bat
```

Unix :
```
$ bin/elasticsearch
```

### Test Installation

http://localhost:9200

Expected results :

```
{
  "name" : "es1",
  "cluster_name" : "elasticsearch",
  "version" : {
    "number" : "2.3.4",
    "build_hash" : "218bdf10790eef486ff2c41a3df5cfa32dadcfde",
    "build_timestamp" : "2016-05-17T15:40:04Z",
    "build_snapshot" : false,
    "lucene_version" : "5.5.0"
  },
  "tagline" : "You Know, for Search"
}
```
