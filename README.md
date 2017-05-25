Rock.Logging
============

## Logging Levels

### Debug 
This would be for messages that are useful for debugging an applciation.

### Info
This would be for messages that are useful for support staff to help determine context or source of error

### Warn
This would be used to provide a warning that something unexpected or fatal could be possible

### Error
This would be used to provide detailed information about a system error

### Fatal
This would be used to provide detailed information about the state of the system after a crash causing error

### Audit
This would be used for messages that are needed for audit purposes.  This is a bit like Debug, but for non-technical use

## Logging Adapters
* Console Log Provider
* Email Log Provider
* File Log Provider
* Formattable Log Provider
* Http Endpoint Log Provider
* Rolling File Log Provider

## Configuration Examplss
* [Basic Configuration](docs/BasicConfig.md)
* [Advanced Confiuration](docs/AdvancedConfig.md)
  * [Custom Formatter Configuration](docs/AdvancedConfig.md#Custom-sFormatter-Configuration)
  * [IoC Configuration](docs/AdvancedConfig.md#IoC-Configuration)

