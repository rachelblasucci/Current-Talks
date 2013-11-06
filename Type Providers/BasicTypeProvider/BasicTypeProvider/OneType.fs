namespace BasicTypeProviders

open System.Reflection
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<TypeProvider>] // <- attribute
type OneTypeProvider(config: TypeProviderConfig) as this = 
    
    inherit TypeProviderForNamespaces() // <- include ProviderImplementation.ProvidedTypes and inherit

    let makeType = 
        let myType = ProvidedTypeDefinition(Assembly.GetExecutingAssembly(), //assembly
                                            "Oredev.OneType", //namespace
                                            "OnePropertyType", //class name
                                            Some typeof<obj>) //base type

        let myProperty = ProvidedProperty("HelloProperty",  //property name
                                    typeof<string>, //property type
                                    IsStatic = true, //parameters..
                                    GetterCode = (fun args -> <@@ "Hello" @@>))

        myType.AddMember myProperty

        do this.AddNamespace("Oredev.OneType", [myType]) // <- add to namespace

[<assembly:TypeProviderAssembly>] // <- assembly attribute
do()
