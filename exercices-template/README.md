## Création d'un template avec alias
```
PUT _template/utilisateurs
{
  "template": "utilisateurs-*",
  "mappings": {
    "mapping1": {
      "properties": {
        "prenom": {
          "type": "keyword"
        },
        "nom" : {
          "type" : "keyword"
        },
        "age" : {
          "type" : "integer"
        },
        "email" : {
          "type" : "keyword"
        }
      }
    }
  },
  "aliases" : {
    "utilisateurs" : {}
  }
}
```

### Récupération des templates

```
GET _template
```

### Insertion d'une donnée dans un index non existant

```
PUT utilisateurs-v1/mapping1/daniellavoie
{
  "prenom" : "Daniel",
  "nom" : "Lavoie",
  "age" : 29,
  "email" : "daniel.lavoie@invivoo.com"
}
```

### Requête par l'alias

```
GET utilisateurs/_search
```

### Insertion d'une donnée dans un autre index non existant

```
PUT utilisateurs-v2/mapping1/daniellavoie
{
  "prenom" : "Bobby",
  "nom" : "Jones",
  "age" : 45,
  "email" : "bobby.jones@jaifaim.com"
}
```

### Modification de l'alias pour pointer ne pointer que sur le nouvel index

```
POST /_aliases
{
    "actions" : [
        { "remove" : { "index" : "utilisateurs-v1", "alias" : "utilisateurs" } }
     ]
}
```
