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

- **Details Page:**
   A detail page that opens when the products are clicked has been created.

- **Hot New:**
    A 'Hot New' page has been added, listing the latest 3 products from each category.

## Project Features (Detailed)
- **Navigation Property and Entity Relationship:**
  In our created project, we established a one-to-many relationship between each instrument and its category and vice versa. This is because we will create multiple instruments within each category.
  To achieve this, we used navigation properties:
    - Within the category, there is a public List<Instrument> property representing the instruments it contains.
    - Within the instrument, there is a public Category property representing the category it belongs to.
  
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

- **Details Page**
    - Instrument Controller:
        - Added a new HTTP GET action method named Details in the InstrumentController.
        - Implemented a route pattern to handle requests for instrument details based on their unique id.
        - Retrieved the instrument details from the database using the provided id.
        - Returned the instrument data to the corresponding view for rendering.

    - Details View (index.cshtml):
        - Created a dedicated view named index.cshtml to display instrument details.
        - Utilized the Instrument model for binding data.
        - Designed the view layout, showcasing the product image, name, description, and price.
        - Implemented a "Buy Now" button for users to initiate a purchase action.

    These changes enable the creation of a detail page that opens when users click on products, enhancing the user experience by providing detailed information about the selected instrument and offering a straightforward option to make a purchase.

- **Hot New**
    - HotNewController:
        - Created a new controller named HotNewController responsible for managing hot new products.
        - Dynamically fetched the latest 3 instruments from each category, ordered by descending creation date and then ascending price.
        - Combined and displayed these instruments in the Index view.
          
    - Index View (HotNew):
        - Modified the Index view under the HotNew section to integrate the retrieved instrument data.
        - Each instrument's image, name, and price were displayed within styled cards for a visually appealing presentation.
        - Implemented a card footer with a "Details" link for users to access detailed information about each instrument.

    These changes enhance user experience by showcasing new and trending products effectively, encouraging user engagement and exploration of the featured instruments.

## Assignment of Tasks
- **Elif Baykara**
    - Navbar Redesign and Optimization: Created a new navbar featuring links to "Home" and "Add Instrument." Implemented page redirections for these links. Optimized the navbar for improved user experience.
    - Instrument Detail and Addition Pages: Developed detail pages for instruments and an instrument addition page, both linked to brand and model information. Completed frontend components for these pages. Established a database connection in the Instrument Controller. Implemented CRUD operations for listing, adding, and updating instruments.
    - Home Page Enhancement: Implemented card structures to list all instruments on the home page. Added links to respective detail pages within the card structures. Included a "Buy Now" button for direct purchase options. Displayed instruments with images, prices, and names. Integrated a description section on the instrument detail page. Implemented CSS styles for the detail page and applied necessary styles to "Home.css."
    - Logo Design and Integration: Designed a logo for ReDo Music. Created an "img" folder under the "wwwroot" directory to store the PNG format logo. Wrote CSS code for the logo, ensuring proper integration within the navbar.
  
- **Esmanur Mazlum**
    - Navigation Property: Implemented relationships between entities for efficient data retrieval and navigation.
    - Category Controller and Category View: Developed controllers and views for managing and displaying product categories.
    - Ascending Price Order: Implemented functionality to list instruments based on their price in ascending order, providing users with a sorted view based on price.
    - Search Bar Activation: Enabled the search bar feature for users to find specific items within the application.
    - Hot New Controller and View: Created a controller and view to showcase trending or newly added products.
    - GitHub - Merge: Collaborated on GitHub by merging code changes and branches for seamless integration.
      
- **Cengizhan Civelek**
    - Creation of Entities: Under the Core layer of the project, entities such as Brand, Instrument, and Category are defined.
    - Entity Inheritance: These entities inherit properties and behaviors from the base entity, establishing connections and relationships among them.
    - Product Listing: The Home controller is responsible for retrieving and displaying products.
    - Index View Creation: An Index view is created under the Home controller to list the products.
    - Filtering Options: User-friendly filtering options are implemented, including price intervals, colors, and categories.
    - Checkbox Models: Models are defined for checkboxes corresponding to price intervals, colors, and categories.
    - HTML Code Implementation: HTML code is written within the Index view to integrate these checkbox models, enabling seamless user interaction and filtering.
    - Database Setup: Configuration and definition of instruments within the database, ensuring accurate storage and retrieval of instrument-related data.
      
- **Zehra Bengisu Doğan**
    - Brand CRUD Operations: Implemented CRUD operations for the Brand entity, allowing Create, Read, Update, and Delete operations.
    - Brand View Enhancement: Added a view page displaying a list of brands and enabling update and delete functionalities.
    - Home Controller Optimization: Optimized the Home controller to ensure correct retrieval and listing of instruments based on selected categories in checkboxes.
    - Fixed Enum Values: Prevented the issue of color options showing a default value of 0 in checkboxes upon page refresh.
    - Home Controller Post Action Update: Enhanced the Home controller's post action to prevent multiple submissions of the same category, updating the index.cshtml accordingly.
    - Database: Adding Instruments and Related Data: Integrated functionality to add instruments, categories, brands, and image URLs to the database.
    - ReadMe Documentation: Drafted and structured the ReadMe document you are currently reading :) providing comprehensive project information and instructions.


## Problems We Had Experienced
- We encountered an issue using the color enum in filtering within our project. After selecting a color, the values were returning as 0, and the desired list output was not obtained.
- When a new instrument was added, even if an existing category and color were selected, duplicate categories and colors with the same name were still being created in the checkbox field.
- After establishing the relationship between instrument and category, an error occurred during the migration process in the public key definition, and EF Core had difficulty understanding the relationship we implemented.
  - To resolve this issue, we specified the relationship and public key manually within the 'OnModelCreating' method in the ReDoMusicDbContext class.


## Potential Project Additions Ahead
- **Adding API Layer:**
   - We aim to enhance communication with the external world by adding an API layer to our project. This will enable the project to exchange data with other applications and broaden its usability.
     
- **JWT-Based User Authentication and Authorization:**
    - We are considering adding user authentication and authorization features. By using JSON Web Tokens (JWT), we can make our project more dynamic, secure, and user-friendly for various operations.

- **Adding a Blazor Web Project:**
  - To improve the performance of our project's website, we are contemplating the use of server-side or client-side Blazor technology. This will enhance the user experience, making the project more appealing.
    
- **Creating a Shopping Cart and Order Page:**
    - We intend to add a shopping cart and an order page to facilitate users in easily adding products to their carts and completing their orders. This transformation will help us turn the project into an e-commerce platform.
    
