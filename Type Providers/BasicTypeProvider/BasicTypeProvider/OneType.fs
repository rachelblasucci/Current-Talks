namespace BasicTypeProviders

open System.Reflection
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<TypeProvider>]
type OneTypeProvider(config: TypeProviderConfig) as this = 
    
    inherit TypeProviderForNamespaces()

    let makeType = 
        let myType = ProvidedTypeDefinition(Assembly.GetExecutingAssembly(), //assembly
                                            "Samples.OneType", //namespace
                                            "OnePropertyType", //class name
                                            Some typeof<obj>) //base type

        let myProperty = ProvidedProperty("HelloProperty",  //property name
                                    typeof<string>, //property type
                                    IsStatic = true, //parameters..
                                    GetterCode = (fun args -> <@@ "Hello" @@>))

        myType.AddMember myProperty

        do this.AddNamespace("Samples.OneType", [myType])

[<assembly:TypeProviderAssembly>] 
do()
