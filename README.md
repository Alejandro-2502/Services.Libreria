# Services.Libreria
Un Ejemplo de un Servicio, aplicando patrones de diseño, Clean Architecture, Security JWT, FluentValidation, UnitTest con Mock y RedisCache, entre otras características

Se implemento el patron (Clean Architecture). Se aplica, para este desarrollo Principios SOLID.
Para el acceso a la base de datos, se emplea OMR ( DAPPER ). Ademas se aplica Inyeccion de Dependencias (DI) 
Tambien, se utiliza (FluentValidation), para aplicar validaciones basicas a el INPUT del request.
Tambien, se agregan Unitets. ( MSTest - Mock)
Tambien se agrega la utilidad de una base de datos de tipo Cache ( RedisCache )
Por ultimo se utiliza, para brindar seguridad, en los endpoint ( JWT - Json Web Token )

El diseño empleado consta de las siguientes capas que se mencionan a continuación

Capa Principal Servicio .API:

- La misma contiene los controladores
  
  - LibroCommandController
  - LibroQuerysController
  - TokenController ( Se genera el token, para el acceso a los endpoints )
  
- Tambien se encuentra alojado las clases, propias de configuraciones de ( App y Services ). La idea de realizarlo asi, era mantener segmentada, 
  para una mayor claridad y para la utilidad de ( EXTENSIONES ) en las configuraciones requeridas, para este Api Services. 
  
  - Extensiones
	- IApplicationBuildExtension ( clase Statica )
	- IServiceCollectionExtensions ( clase Statica )
	- IInjectionsExtensions ( clase Statica )
	
- Archivos de Appsetting (Dentro se encuentra la conexion a la DB, como asi tambien los parametros, necesarios, para confeccionar la estructura de JWT y Redis Cache )

Capa de aplicación:

Se encuentra toda la estructura que se podrá utilizar, dentro de las otras capas, si así fuese necesario. Dentro de esta capa, están alojados dentro de directorios, para un mayor ordenamiemto y disponibles lo siguiente:

	- Configurations
 		- ConfigHelper
   		- ConfigJwt
     		- ConfigSqlServer
       		- ServerRedis
	 	- TTLCacheRedis
	- DTO
	- Gateways
 		- ILibroCommandGateway
   		- ILibroQuerysGateway
     		- IUsuarioGateway
       
	- Genérics	
 		- Responses<T>
	- Helpers
 		- Token
   			- TokenHelper
	- Interactor
		- Common
  			- LogServicesInteractor
  		- Redis
    			- DistributedRedisCacheInteractor
		- Token
  			- TokenInteractor
  		- LibroCommandInteractor
    		- LibroQuerysInteractor
      		- UsuarioInteractor
		- ValidationsInteractor
	
	- Interfaces
		- ICommon
  			- ILogServicesInteractor
     		- Redis
       			- IDistributedRedisCacheInteractor
		- Token
  			- ITokenInteractor
     		- ILibroCommandInteractor
       		- ILibroQuerysInteractor
	 	- IUsuarioInteractor
   		- IValidationsInteractor
	
	- Mappers 
		- MapperProfiles (Se emplea Automapper)
		- Token ( Se emplea un mapper manual )
  			- TokenResponseMapper
		
	- Messages : ( Se emplea archivos de recursos, para el contenido de los mensajes de validaciones y advertencias, que se puedan producir durante la ejecución del flujo)
	
	- Request ( Clase empelada, para los datos y estructura, que será empleada en el input de los endpoint )
	- Responses ( Clases creadas de tipo Genericas, para estandarizar, lo ma optimo posible, la salida a respuestas de las solicitudes de cada endponit )
 		- Common
   			- Response
      		- Token
			- TokenResponse
   		- LibroResponse
     		- UsuarioResponse
       
	- Validations ( Contiene las validaciones basicas, empleando FluentValidation, aplicado al Request )
 		- LibroValidations
		
Capa Infraestructura:

	- Dentro de esta capa, se encuentra toda la estructura, que se empleará para la base de datos SQLServer, utilizando OMR Dapper. 
	  Tambien contiene un archivo recurso ( Scripts ), donde se encontraran alojados los Querys, que seran empleado en esta capa de Accesos a DB, mediante DAPPER
	- DAOs
	  - Helper
   		- CreateParameters
	  - LibroDAO
   		- LibroCommandDao
     		- LibroQuerysDao
	  - UsuarioDAO
   		- UsuarioDAO
	  
Capa Testing:

	- Dentro de esta capa, se creo para realizar las puebas unitarias (MSTest),  para este caso, se crearon los casos mediante Mock, para la clase 
	  que realiza las operaciones de busquedas ( QuerysLibro ).
	  
	-  Libro
	
		- Command
			- CommandData ( Falta completar )
			- CommandTest ( Falta completar )
			
		- Querys
			- QuerysData ( Se obtiene los datos, para realizar las pruebas )
			- QuerysTest ( Se crean las pruebas unitarias, con los datos Moqueados )
				Casos:
					- GetAll - OK
					- GetAll - NotFound
					- GetAll - InternalServerError
			
Nota: Como una simple aclaracion, es tan solo un simple ejemplo, de una forma de muchas existentes, para realizar este desarrollo. Es tan solo a modo de ejemplo

Saludos.! Gracias
