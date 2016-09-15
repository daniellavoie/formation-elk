# Setup a cluster

### Launch from Linux

Instance 1 :

```
$ bin/elasticsearch -Dcluster.name='elk-training' \
-Dhttp.port=9200 \
-Dtransport.tcp.port=9300 \
-Dpath.data=data/instance1 \
-Dnode.name=es1 \
-Dhttp.cors.enabled=true \
-Dhttp.cors.allow-origin=* \
-Ddiscovery.zen.ping.unicast.hosts=localhost:9301,localhost:9302
```

Instance 2 :

```
$ bin/elasticsearch -Dcluster.name='elk-training' \
-Dhttp.port=9201 \
-Dtransport.tcp.port=9301 \
-Dpath.data=data/instance2 \
-Dnode.name=es2 \
-Dhttp.cors.enabled=true \
-Dhttp.cors.allow-origin=* \
-Ddiscovery.zen.ping.unicast.hosts=localhost:9300,localhost:9302
```

Instance 3 :

```
$ bin/elasticsearch -Dcluster.name='elk-training' \
-Dhttp.port=9202 \
-Dtransport.tcp.port=9302 \
-Dpath.data=data/instance3 \
-Dnode.name=es3 \
-Dhttp.cors.enabled=true \
-Dhttp.cors.allow-origin=* \
-Ddiscovery.zen.ping.unicast.hosts=localhost:9300,localhost:9301
```

### Launch from Windows

Instance 1 :

```
$ bin/elasticsearch -Dcluster.name='elk-training' ^
-Dhttp.port=9200 ^
-Dtransport.tcp.port=9300 ^
-Dpath.data=data/instance1 ^
-Dnode.name=es1 ^
-Dhttp.cors.enabled=true ^
-Dhttp.cors.allow-origin=* ^
-Ddiscovery.zen.ping.unicast.hosts=localhost:9301,localhost:9302
```

Instance 2 :

```
$ bin/elasticsearch -Dcluster.name='elk-training' ^
-Dhttp.port=9201 ^
-Dtransport.tcp.port=9301 ^
-Dpath.data=data/instance2 ^
-Dnode.name=es2 ^
-Dhttp.cors.enabled=true ^
-Dhttp.cors.allow-origin=* ^
-Ddiscovery.zen.ping.unicast.hosts=localhost:9300,localhost:9302
```

Instance 3 :

```
$ bin/elasticsearch -Dcluster.name='elk-training' ^
-Dhttp.port=9202 ^
-Dtransport.tcp.port=9302 ^
-Dpath.data=data/instance3 ^
-Dnode.name=es3 ^
-Dhttp.cors.enabled=true ^
-Dhttp.cors.allow-origin=* ^
-Ddiscovery.zen.ping.unicast.hosts=localhost:9300,localhost:9301
```

### Check the cluster state

http://localhost:9200/_cluster/health

Expected result :
```
{ "cluster_name":"elk-training",
  "status":"green",
  "timed_out":false,
  "number_of_nodes":3,
  "number_of_data_nodes":3,
  "active_primary_shards":0,
  "active_shards":0,
  "relocating_shards":0,
  "initializing_shards":0,
  "unassigned_shards":0,
  "delayed_unassigned_shards":0,
  "number_of_pending_tasks":0,
  "number_of_in_flight_fetch":0,
  "task_max_waiting_in_queue_millis":0,
  "active_shards_percent_as_number":100.0
}
```
