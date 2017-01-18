# Déroulement

## Jour 1

* Introduction
* Elasticsearch
* Logstash
* Kibana
* Beats

## Jour 2

* Administration

## Jour 3

* Bonnes pratiques
* Mise en pratique d'une architecture avancé

# Plan de Cours

## INTRODUCTION

### Matière

* L’écosystème d’Elasticsearch
* Le rôle d’Elasticsearch, Logstash, Kibana et Beats
* Exemples d’architectures
* Cas d’utilisations
* Simplifier la gestion des versions avec The Elastic Stack V5
 
## ELASTICSEARCH

### Matière

* Introduction à Elasticsearch
* Coeur d'Elasticsearch
* API
* Node client vs Transport client
* Les compositions d'un cluster
* DEMO : Création d'un cluster
* EXERCICE : Installation d'Elasticsearch
* Gestion de la configuration
* EXERCICE : Configuration d'un cluster Elasticsearch
* Terminologie
* Types de recherches
* Indexation
* Modifications en lots
* Mapping et analyse
* Analyse
* Mapping
* Agrégations
* EXERCICE : Requêtage avec QueryDSL
* EXERCICE : Expérimentation sur les mappings

### Exercices

* Requêtage avec QueryDSL
* Expérimentation de mappings
 
## LOGSTASH

### Matière

* Concepts: Input, Output, Filter (filtre), Codecs…
* Les Inputs: File, Redis, RabbitMQ…
* Les Filters: Grok, Date, Mutate…
* Les Outputs: File, Elasticsearch, Redis…
* Threading et haute-disponibilité

### Exercices

* Installation de logstash
* Intégration d'un jeu de donnée avec Logstash
* Consommation de logs d'une application Java
 
## KIBANA

### Matière

* Installation et configuration
* Découverte des données et construction des requêtes / Queries
* Agrégations et construction de Visualizations
* Panels
* Création des vues
* Mise en place d’un tableau de bord
 
### Exercices

* Installation de Kibana
* Création d'un dashboard
 
## BEATS

### Matière

* Introduction aux Data Shippers et au monitoring temps réel
* Monitorez votre réseau grâce à PacketBeat
* Monitorez vos fichiers grâce à FileBeat
* Monitorez vos Windows event logs grâce à WinlogBeat
* Récupérer les métriques importantes de vos serveurs grâce à Metricbeat

### Matière

* Installation de Metricbeats
 
## ADMINISTRATION

### Matière

* X-Pack
* Sauvegardes : Snapshots et Restore
* Monotiring du cluster
* Alerting
* Tuning et architectures avancés
* Templates et indices

### Exercices

* Installation de X Pack
* Configuration d'alertes
* Gestion de snapshots
* Sécurisation d'indexes
* Utilisation des templates et des indices

## Bonnes pratiques

### Matière

* Configuration pour la production
* Architectures avancé
* Consignes sur le hardware
* Gestion de données timeseries

### Exercices

* Installation d'une architecture respectant les bonnes pratiques.
