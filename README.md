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

- **Search Bar:**
  Allows users to search products by their names, ensuring quick and precise results for effortless navigation.

## Project Features (Detailed)
- **Navigation Property and Entity Relationship:**
- **Controller and Views for CRUD Operations:**
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

-	**Instrument Filtering with Checkbox Model:**
    - Controller (HomeController):
      - HTTP GET Index Method:      
          - Executes when the user visits the homepage of the application. The purpose of this method is to present the user with a form containing filtering options. This form includes categories, colors, and price intervals. Categories, colors, and price intervals are fetched from the database and transformed into CheckboxViewModel, ColorViewModel, and PriceIntervalViewModel objects.     
      - HTTP POST Index Method:
          - Executes when the user fills out and submits the form. The purpose of this method is to process the data from the form and perform filtering operations. Selected categories, colors, and price intervals are used to fetch data from the Instruments table, and this data is returned to the user in the CategoryColorViewModel object.
            
    - Model (CategoryColorViewModel):
         - Categories, Colors, PriceIntervals:
             - Stores the user's selections and filtering options. After an HTTP POST request, the Controller uses these properties to fetch data from the Instruments table.
        - Instruments:
             - Stores the list of filtered instruments. Instruments that match the criteria specified by the user are stored in this property.
               
     - Database Relationships (ReDoMusicDbContext and Entity Classes):
       - Instruments, Categories:
         - Each instrument in the Instruments table has a reference to a category in the Categories table. This relationship is defined as a property of Category type within the Instrument class. As a result, each instrument in the Instruments table belongs to a category.

    - View (Home/Index.cshtml):
        - Form Design:
            - Contains an HTML form that presents filtering options to the user. Categories, colors, and price intervals are displayed as checkboxes. When the user fills out and submits the form, this data is sent to the Controller via a POST request.

         - Foreach Loop:
             - Contains a loop that lists the filtered instruments. If each instrument matches the criteria specified by the user, it is displayed in a list format using this loop.
       
  During this process, the Controller, Model, and View interact with each other to fetch data from the database based on the user's request, process this data, and present the results to the user. Thanks to the database relationships, the data in the Instruments table is linked to the categories in the Categories table through references, and filtering operations are performed based on these relationships. This allows users to find instruments in the application that match their desired criteria.

- **Search Bar**
Here is how the method works:
  - [HttpGet] public IActionResult Search(string searchTerm = ""):
      - This HTTP GET method takes a search term (searchTerm) as a parameter from the user. It is considered as an empty string by default, meaning it shows all instruments and categories if the user is not searching for anything.
   - Database Queries:
       - _dbContext.Categories.ToList():** Retrieves all categories from the database.
       - _dbContext.Instruments.Include(x=>x.Category):** Retrieves instruments along with their categories.
       - Filters items containing the search term in instrument names or category names using the Where statement.
       - Orders instruments by price using OrderBy(x => x.Price).

  - Creating Category, Color, and Price Range Options:
      - categoriesWithColors.Select(...):** Converts categories into CheckboxViewModel objects.
      - Enum.GetValues(typeof(Color)).Cast<Color>():** Takes color options from the Color enum and converts them into ColorViewModel objects.
      - Price intervals (PriceIntervalViewModel) are defined manually.
 
  - Creating ViewModel:
     - var viewModel = new CategoryColorViewModel { ... }:** Creates a CategoryColorViewModel object containing the created category, color, price range options, and filtered instruments.

  - Sending to the View:
      - return View("Index", viewModel);:** Sends the created viewModel object to the view named "Index". This allows the user to view the search results and filtering options on the page.
      - This method prepares the necessary data for the user to perform searches, view the results, and see filtering options, and it passes this data to the View layer for display on the screen.

## Assignment of Tasks

## Problems We Had Experienced

## Potential Project Additions Ahead
