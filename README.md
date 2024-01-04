# Back
## La solución del back esta diseñada en diferentes capas en las cuales se utiliza los siguiente frameworks:
### 
* .Net 8
* AspNetCoreHero.Results
* AutoMapper
* Bogus
* MediatR
* EntityFrameworkCore
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Swashbuckle.AspNetCore
## Se realizaron migraciones
```
Add-Migration InitialCreate

Update-Database
```
## Estos son los EndPoint creados 
```
/api/User/GetAll/{test} -- Obtiene todos los registros de la BD o con el parámetro test=true obtiene 100 registros aleatorios
/api/User/{id} -- Obtiene todos los registro por id
/api/User/Post -- Crea un registro
/api/User/Put -- Actualiza un registro
/api/User/Delete/{id} -- Elimina un registro
/api/User/PostAllTest -- Genera 100 registros en la BD (Se basa en los métodos GetAll: test=true y Post)
```
<img width="1064" alt="Captura de pantalla 2024-01-04 121251" src="https://github.com/Chisfx/Demo/assets/101854771/00339f78-2df0-4db6-bfc1-9525498fc66f">

## Ejecutar IIS

# Front

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).
