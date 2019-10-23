# RecipeBox

Logoin to  create and store recipes via categories. This is a user specified application. 

## Authors

Loewy Malkovich, loewymalkov@gmail.com
Hailey Gaylor, Haileygaylor@gmail.com

## Use
User can add recipes and cooking instructions or view their current recipes with cooking instruction by personal tags. Tags are used to categorize the recipes for user prefernce of food.
## Set-Up

- run 'git clone https://github.com/loewymalkov/RecipeBox.git' in terminal to copy directory
- navigate to project directory 'RecipeBox' and run 'dotnet restore', 'dotnet ef database update'
- 

## Specs

| Scenario | When given that | Result |
|-|-|-|
| user can add recipe with ingredients and instructions | "Cheeseburger", "cook burger", "patty, buns, tomato"| Recipe for Cheeseburger; "Instructions: cook burger"; "Ingredients: patty, buns, tomato" |
| user can add tags to a recipe | "American" | "Cheeseburger tags: American"|
| user can update and delete tags | user clicks 'edit' or 'delete' links |  deletes tag from database |
| user can edit recipe | "Bacon Cheeseburger", "bacon" | "Recipe for Bacon Cheeseburger"; "Instructions: cook burger"; "Ingredients: patty, buns, tomato, bacon" |
| user can delete recipe | 'delete' | delete recipe from database |
| user can rate recipe | "5" | "Bacon Cheeseburger: *****" |
| user can sort recipes by rating | 'order by rating' | orders by star rating (ascending) |


## Technologies

C#, EntityFramework, .NET, MySQL Workbench, VS Code, .cshtml;

## License

Open source, 2019 (MIT)