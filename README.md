# Services.Libreria
Un Ejemplo de un Servicio, aplicando patrones de diseño, Clean Architecture, Security JWT, FluentValidation, entre otras características

Se implemento el patron (Clean Architecture). Se aplica, para este desarrollo Principios SOLID.
Para el acceso a la base de datos, se emplea OMR ( DAPPER ). Ademas se aplica Inyeccion de Dependencias (DI) 
Tambien, se utiliza (FluentValidation), a fin de aplicar validaciones basicas a el INPUT del request.
Por ultimo se utiliza, para brindar seguridad, en los endpoint ( JWT - Jason Web Token ).

El diseño empleado consta de las siguientes capas que se mencionan a continuación:

Capa Principal Servicio .API - (Presenter):

- La misma contiene los controladores.
  
  - LibroCommandController
  - LibroQuerysController
  - TokenController ( Se genera el token, para el acceso a los endpoints )
  
- Tambien se encuentra alojado las clases, propias de configuraciones de ( App y Services ). La idea de realizarlo asi, era mantener segmentada, 
  para una mayor claridad y para la utilidad de ( EXTENSIONES ) en las configuraciones requeridas, para este Api Services. 
  
  - Extensiones :
	- IApplicationBuildExtension ( clase Statica )
	- IServiceCollectionExtensions ( clase Statica )
	- IInjectionsExtensions ( clase Statica )
	
- Archivos de Appsetting: Dentro se encuentra la conexion a la DB, como asi tambien los parametros, necesarios, para confeccionar la estructura de JWT

Capa de aplicación:

Se encuentra toda la estructura que se podrá utilizar, dentro de las otras capas, si así fuese necesario. Dentro de esta capa, están alojados dentro de directorios, para un mayor ordenamiemto y disponibles lo siguiente:

	- Configurations
	- DTO
	- Gateways
	- Genérics
	- Helpers
	- Interactor
		- Common
		- Token
	
	- Interfaces
		- ICommon
		- Token
	
	- Mappers 
		- MapperProfiles (Se emplea Automapper)
		- Token ( Se emplea un mapper manual )
		
	- Messages : ( Se emplea archivos de recursos, para el contenido de los mensajes de validaciones y advertencias, que se puedan producir durante la ejecución del flujo)
	
	- Request ( Clase empelada, para los datos y estructura, que será empleada en el input de los endpoint )
	- Responses ( Clases creadas de tipo Genericas, para estandarizar, lo ma optimo posible, la salida a respuestas de las solicitudes de cada endponit )
	- Validations ( Contiene las validaciones basicas, empleando FluentValidation, aplicado al Request )
		
Capa Infraestructura:

	- Dentro de esta capa, se encuentra toda la estructura, que se empleará para la base de datos SQLServer, utilizando OMR Dapper. 
	  Tambien contiene un archivo recurso ( Scripts ), donde se encontraran alojados los Querys, que seran empleado en esta capa de Accesos a DB, mediante DAPPER
	- DAOs
	  - Helper
	  - LibroDAO
	  - UsuarioDAO
	  
Nota: Como una simple aclaracion, es tan solo un simple ejemplo, de una forma de muchas existentes, para realizar este desarrollo. Es tan solo a modo de ejemplo

Saludos.! Gracias
