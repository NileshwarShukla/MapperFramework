# MapperFramework
The framework supports feature of mapping of one entity to another. It needs a source entity, a target entity and mapping defined in JSON file.
It is completely dynamic framework which transform value from source object to target object.

The mapping can be done by following ways:-

•	Basic: Direct static value assignment.

•	Ref : Using direct reference of parent entity.

•	Offset: Referring to parent entity with some offset value.

•	Hook: Refering to custom method which resolves the mapping value.

1) Input Source Object

{
	Prop1:Test
	DateAdded:20-06-2023 09:09:47
	Area:Country
	User:Nilesh
	Address:{
		Address:Val1
		Address:Val2
		Address:Val3
                }
}




2) We can declare mappings in JSON in following way:-
{
  "mappings": [
    {
      "targetFieldName": "Title", 
      "sourceFieldName": "Title",
      "basic": {
        "value": "TitleTest"  // mapping defined for direct assignment
      }
    },
    {
      "targetFieldName": "Area",
      "sourceFieldName": "Area",
      "hook": { "methodName": "AssignArea" }  //AssignArea is custom method that set target value string.Format("{0} {1}", o.Area,"Added"); 
    },
    {
      "targetFieldName": "Assignee",
      "sourceFieldName": "Prop1",
      "ref": {
        "type": "System.String"

      }
    },
    {
      "targetFieldName": "Address",
      "sourceFieldName": "Address.Address1",
      "ref": {
        "type": "System.String"

      }
    },
    {
      "sourceFieldName": "DateAdded",
      "targetFieldName": "DueDate",
      "offset": {                            // Due Date  will be 28 Days ahead from DateAdded
        "type": "System.DateTime",
        "value": 28
      }
    },

    {
      "targetFieldName": "Comments",
      "hook": {                               //nested mapping structure
        "methodName": "AssignDescription",    // So the source "user's" value would be appended to the outcome of method "AssignDescription"
        "sourceFieldName": "User",            // string.Format("{0} {1}", "Source Comments",o.Address.Address2);
        "ref": {
          "type": "System.String"
        }
      }

    }

  ]

}

3) Output of Generated Assignment object

{
"Title":"TitleTest",
"Area":"Country Added",
"Assignee":"Test",
"DueDate":"2023-07-18T09:09:47",
"Comments":"Source Comments Val2 Nilesh",
"Address":"Val1"
}