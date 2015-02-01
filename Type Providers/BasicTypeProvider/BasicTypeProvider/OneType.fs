namespace BasicTypeProviders

open System.Reflection
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<TypeProvider>] // <- add attribute
type OneTypeProvider(config: TypeProviderConfig) as this = 
    
    inherit TypeProviderForNamespaces() // <- include ProviderImplementation.ProvidedTypes and inherit

    let mynamespace = "CodeMash.OneType";

    let makeType = 
        let myType = ProvidedTypeDefinition(Assembly.GetExecutingAssembly(), //assembly
                                            mynamespace, //namespace
                                            "OnePropertyType", //class name
                                            Some typeof<obj>) //base type

        let myProperty = ProvidedProperty("HelloProperty",  //property name
                                    typeof<string>, //property type
                                    IsStatic = true, //parameters..
                                    GetterCode = (fun args -> <@@ "Hello" @@>))

        myType.AddMember myProperty

        do this.AddNamespace(mynamespace, [myType]) // <- add to namespace

[<assembly:TypeProviderAssembly>] // <- add assembly attribute
do()
