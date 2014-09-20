namespace Emailer 
module Data = 
    open Types

    // Hacky data is hacky.
    let GetData = [|
           EmailType1Message {FirstName="Rachel"; LastName="Reese"; Email="rachel@test.org"; Number=1};
           EmailType2Message {FirstName="Jana"; Email="jana@test.org"; Number=2};
           EmailType3Message {Email="julie@test.org"; Number=3};
           EmailType2Message {FirstName="Jill"; Email="jill@test.org"; Number=4};
           EmailType3Message {Email="michelle@test.org"; Number=5};
           EmailType2Message {FirstName="Michael"; Email="mike@test.org"; Number=6};
           EmailType1Message {FirstName="Jenna"; LastName="Reese"; Email="jenna@test.org"; Number=7};
           EmailType1Message {FirstName="Joy"; LastName="Reese"; Email="joy@test.org"; Number=8};
           EmailType1Message {FirstName="Shonna"; LastName="Reese"; Email="shonna@test.org"; Number=9};
           EmailType2Message {FirstName="Rajan"; Email="rajan@test.org"; Number=10};
           EmailType3Message {Email="christie@test.org"; Number=11};
           EmailType1Message {FirstName="Jacobsen"; LastName="Reese"; Email="jacob@test.org"; Number=12};
           EmailType1Message {FirstName="Titus"; LastName="Reese"; Email="titus@test.org"; Number=13};
           EmailType2Message {FirstName="Bethany"; Email="bethany@test.org"; Number=14};
           EmailType1Message {FirstName="Macarthur"; LastName="Reese"; Email="mac@test.org"; Number=15};
           EmailType3Message {Email="bella@test.org"; Number=16};
           EmailType2Message {FirstName="Eliana"; Email="ellie@test.org"; Number=17};
           EmailType2Message {FirstName="Casey"; Email="casey@test.org"; Number=18};
           EmailType2Message {FirstName="Quint"; Email="quint@test.org"; Number=19};
           EmailType2Message {FirstName="Sydney"; Email="sydney@test.org"; Number=20};
           EmailType1Message {FirstName="Sophie"; LastName="Reese"; Email="sophie@test.org"; Number=21};
           EmailType3Message {Email="abby@test.org"; Number=22};
           EmailType1Message {FirstName="Barbara"; LastName="Reese"; Email="barb@test.org"; Number=23};
           EmailType1Message {FirstName="David"; LastName="Reese"; Email="dave@test.org"; Number=24};
           EmailType3Message {Email="albob@test.org"; Number=25};
           EmailType1Message {FirstName="Jon"; LastName="Reese"; Email="jon@test.org"; Number=26};
           EmailType1Message {FirstName="Sandy"; LastName="Reese"; Email="sandy@test.org"; Number=27};
           EmailType1Message {FirstName="Joyce"; LastName="Reese"; Email="joyce@test.org"; Number=28};
           EmailType3Message {Email="dave@test.org"; Number=29};
           EmailType2Message {FirstName="Bud"; Email="bud@test.org"; Number=30};
           EmailType2Message {FirstName="Rob"; Email="rob@test.org"; Number=31};
           EmailType1Message {FirstName="Kevin"; LastName="Reese"; Email="kevin@test.org"; Number=32};
           EmailType2Message {FirstName="Francisco"; Email="francisco@test.org"; Number=33};
           EmailType3Message {Email="paul@test.org"; Number=34};
           EmailType2Message {FirstName="Hunter"; Email="hunter@test.org"; Number=35};
    |]

