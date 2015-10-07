namespace Emailer 
module Types = 
    // EmailType1: Sign-up, what's new, weekly specials
    // EmailType2: Shopping list, daily deals, receipt
    // EmailType3: Forgot password, recipe.

    type Agent<'T> = MailboxProcessor<'T>

    type EmailType1 = {
        FirstName : string;
        LastName : string;
        mutable Email: string;
        Number : int
    }

    type EmailType2 = {
        FirstName : string;
        mutable Email : string;
        Number : int
    }

    type EmailType3 = {
        mutable Email : string;
        Number : int
    }

    type Message = 
    | EmailType1Message of EmailType1
    | EmailType2Message of EmailType2
    | EmailType3Message of EmailType3
    
