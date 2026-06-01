# UnityProjectTest
Unity Project Take Home Test 
      (For Unity developers, if you are more interested in design/3D modeling, please let us know) We can accomodate that as well)

Please understand there is NO FAIL, this is simply to better understand your level of understanding of the UNITY platform. We will be taking a number of candidates for this semester as training and better developing your UNITY skills. All of our 20 plus interns are here for our Friday AM experience walk throughs so we can better align our data (i.e., we need a classroom sized internship to better develop our data from the students). So how much you learn and train from our internship is up to you, but we give you a lot of ability to really learn about developing in a production environment.

# Instructions
1. Send your Github username to Ray so that we can invite you as an external collaborator to this repo. Accept the invitation and follow next steps
2. Clone the repository to your local machine.
3. Create a new branch from main branch and publish your branch
4. Read each question below and work through them.
5. Please commit each answer with comments on which question the commit addresses.  It is OK to go back and change previous answers if you jump around in the test, just note it in your commit. We are most interested in your thinking through the process of doing the homework, so the more you explain why you did what you did is very helpful for us and you.
6. Once you have pushed all the commits to your branch, create a pull request fromm your branch to the main branch to submit your final answers(don't merge the request after creating a pull request). At this stage, you've completed the assignment and we will do a code review and get back to you. 
7. We ask you to take no more than 2 hours on the test and to self report how long you worked on it. If you are struggling on anything, please ask us (chat in linkedin) and we will help you. Again, this is not meant to be a pass/fail environment, but rather gathering information for your strengths and weaknesses in Unity. And the first 3 questions should really go fast for you. The last question is meant to show your design thinking, please walk us through your thought process for the design.

# Other Tips
- The project is using Unity version 2023.2.0f1
- There is light project documentation found in the Assets/Documentation folder that will help you understand the project better
- Please avoid adding 3rd party frameworks to the solution as much as you can.  If you feel one is necessary indicate why in your commit.  Please commit this as a separate commit from any of your code.
- Aim to complete the questions using the style or approach this solution uses
- Developer art is expected, do not waste time finding the perfect art
- You do not have to complete all the questions (you can do the ones you feel comfortable in answering)
- If you partially complete a question it is OK submit that with a comment indicating that it is WIP (work in progress)


# Overview
You have been provided with a copy of the 2D Platformer Microgame project, a small project offered by Unity as part of their learning program.  This will be the basis for the test, all questions pertain to adding or changing this project.  In this test we will present a series of feature requests from a theoretical game designer.


## Question 1
The game designer would like to enable the use of health on the Player so that the player can survive more than one collision with an enemy.  The player already has a health script attached, but it is not being used properly.  Refactor the damage code to use the player health value and to cause X damage on a collision with an enemy that would cause damage, where X is specified on a per enemy basis. Showcasing or showing some sort of feedback of how much health is damaged is a nice to have in this scenario.

## Question 2
Completing question 1 may cause an unintended issue for the designer, once the player collides they will likely collide again immediately.  Give the player 1 second of immunity after a collision with an enemy.

## Question 3
The game designer would like to add a player "gun" weapon.  On a keypress, the Player should fire a "bullet" in whatever direction they are facing.  The player can have unlimited bullets and fire as often as they like.  If the bullet collides with an enemy, it should do Y damage, where Y is set specific to the player.

## Question 4
The game designer would like to create different game modes.  These modes would require a different configuration of game values like player health and starting position.  Implement the necessary changes to allow the game designer to be independent of a developer, add and run new game mode configurations.  It is expected the designer can be working inside unity but will not write any code.

- By using scriptable objects, we can allow any developer to create a platformer for a mode and use the Game Load Manager to load it. You can easily add more things to the Game Mode item as development goes a long and it will still be intuitive for the developer. Easy to expand upon and easy to execute.


