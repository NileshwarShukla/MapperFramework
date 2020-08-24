# MapperFramework
The framework supports feature of mapping of one entity to another. It needs a source entity, a target entity and mapping defined in JSON file.
The mapping can be done by following ways:-

•	Basic: Direct static value assignment.

•	Ref : Using direct reference of parent entity.

•	Offset: Referring to parent entity with some offset value.

•	Hook: Refering to custom method which resolves the mapping value.

We can declare mappings in JSON in following way:-

{
    "mappings":
    [
      {
      
      "targetFieldName": "Title",          // mapping defined for direct assignment
      "sourceFieldName": "Title",
      "basic": {
        "value": "TitleTest"
      }
    },
    ...
    {
    
    "targetFieldName": "Area",    
    "sourceFieldName": "Area",    
    "hook": { "methodName": "AssignArea" }         //AssignArea is custom method that set target value on method outcome
    },
    ...    
     {                                        
      "targetFieldName": "Comments",
      "hook": {                                    //nested mapping structure
        "methodName": "AssignDescription",         // So the source "user's" value would be appended to the outcome of method "AssignDescription"
        "sourceFieldName": "User",
        "ref": {
          "type": "System.String"
        }
      }
      ...
     {
      "sourceFieldName": "DateAdded",         //28 days will be added to source's "DateAdded" value
      "targetFieldName": "DueDate",
      "offset": {
        "type": "System.DateTime",
        "value": 28
      }
    }
    ]
  }
