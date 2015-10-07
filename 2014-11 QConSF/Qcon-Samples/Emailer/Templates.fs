namespace Emailer 
module Templates = 
    open Types
    // hacky templates are hacky. 

    // EMAILTYPE1: Sign-up, what's new, weekly specials
    let EmailType1_Template = @"<html>Thank you %FIRSTNAME% %LASTNAME% for signing up with us! You are signed up at the following email: %EMAIL%. To unsubscribe, click <a href='mysite.org/unsub'>here</a>.</html>"

    let templateType1 (fill:EmailType1) =
        printfn "Message %d templated" fill.Number

        (EmailType1_Template
                    .Replace("%FIRSTNAME%", fill.FirstName)
                    .Replace("%LASTNAME%", fill.LastName)
                    .Replace("%EMAIL%", fill.Email), 
            fill.Email, 
            fill.Number)

    // EMAILTYPE2: Shopping list, daily deals, receipt
    let EmailType2_Template = @"<html>Hello %FIRSTNAME%! We've sent your receipt to the following email: %EMAIL%.</html>" 

    let templateType2 (fill:EmailType2) =
        printfn "Message %d templated" fill.Number

        (EmailType2_Template
                    .Replace("%FIRSTNAME%", fill.FirstName)
                    .Replace("%EMAIL%", fill.Email), 
            fill.Email, 
            fill.Number)

    // EMAILTYPE3: Forgot password, recipe.
    let EmailType3_Template = @"<html>We're sorry you've lost your password. We've sent you a new one to the following email: %EMAIL%.</html>" 

    let templateType3 (fill:EmailType3) =
        printfn "Message %d templated" fill.Number

        (EmailType3_Template
                    .Replace("%EMAIL%", fill.Email), 
            fill.Email, 
            fill.Number)


