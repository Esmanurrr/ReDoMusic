# YetGen & Akbank Backend Jump Project
This project is developed for YetGen Akbank Jump.

# Project Description
In our ReDo Music project, our aim is to list the instruments in a filterable way according to category, color and product price. At the same time, you can search among these instruments with the search bar and you can easily search the categories. You can also access product additions from the Home page.

# **Team 4**
- Elif Baykara: Scrum Master / Developer
- Esmanur Mazlum: Developer
- Cengizhan Civelek: Developer
- Zehra Bengisu Doğan: Developer

## Project Name
-ReDo Music-

## Product Backlog URL
[Team 4 Miro Backlog Board](https://miro.com/app/board/uXjVNVFctnk=/?share_link_id=668822531608)

## Project Features
-	**Navigation Property and Entity Relationship:**
  Established a seamless relationship between Category and Instrument entities using navigation properties.
 	
-	**Controller and Views for CRUD Operations:**
  Implemented dedicated controllers and views for Category and Instrument entities, enabling Create, Read, Update, and Delete operations
 	
-	**Instrument Filtering with Checkbox Model:**
  Implemented a checkbox model for efficient instrument filtering, allowing users to narrow down their search criteria and view instruments accordingly.
These features enhance the project's functionality, providing users with a seamless and efficient experience for managing and filtering instruments.

## Project Features (Detailed)
- **Navigation Property and Entity Relationship:**
  - Add Operation:
      - [HttpGet] public IActionResult Add(): Returns a page displaying a form for users to add a new brand. This method works with HTTP GET requests and returns a view.
      - [HttpPost] public IActionResult Add(string brandName, string brandDisplayingText, string brandAddress): After the user fills out the form, this method operates with an HTTP POST request. It creates a new Brand object using the incoming data and adds this object to the database. Then, it saves the added brand to the database and returns a view (which could possibly be a view clearing the same form) after the process is completed.
        
  - Read Operation (Index Action):
      - [HttpGet] public IActionResult Index(): Retrieves all brands from the database and returns them as a list. This operation works with HTTP GET requests and returns a view containing the list of brands.
  
  - Update Operation (Update Action):
       - [HttpGet] public IActionResult Update(string id): Displays a form to the user containing the details of the brand to be updated. This method works with HTTP GET requests and returns a Brand object containing the information of the brand to be updated.
       - [HttpPost] public IActionResult Update(string id, string brandName, string brandDisplayingText, string brandAddress): After the user fills out the form, this method operates with an HTTP POST request. It creates an updated Brand object using the incoming data and updates the relevant brand in the database. Then, it redirects to the Index action after the update process is completed.

   - Delete Operation (Delete Action):
       - [Route("[controller]/[action]/{id}")] public IActionResult Delete(string id): Takes the ID of the brand to be deleted, finds the corresponding brand in the database, and deletes it from the database. Then, it redirects to the Index action after the process is completed.

Through these CRUD operations, users can add, view, update, and delete brands from the database.

## Assignment of Tasks

## Problems We Had Experienced

## Potential Project Additions Ahead
