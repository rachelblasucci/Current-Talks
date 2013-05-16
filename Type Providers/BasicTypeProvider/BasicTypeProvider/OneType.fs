namespace BasicTypeProviders

open System.Reflection
open Samples.FSharp.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<TypeProvider>]
type OneTypeProvider(config: TypeProviderConfig) as this = 
    
    inherit TypeProviderForNamespaces()

    let makeType = 
        let myType = ProvidedTypeDefinition(Assembly.GetExecutingAssembly(), 
                                            "Samples.OneType", 
                                            "OnePropertyType", 
                                            Some typeof<obj>)

        let myProperty = ProvidedProperty("Name", 
                                    typeof<string>, 
                                    IsStatic = true, 
                                    GetterCode = (fun args -> <@@ "Hello" @@>))

        myType.AddMember myProperty

        do this.AddNamespace("Samples.OneType", [myType])

[<assembly:TypeProviderAssembly>] 
do()
