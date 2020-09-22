# Battleships sample project
## Introduction

This repository contains the complete example of one-sided Battleships game. One-sided means in that case that human player will be able to try to hit and destroy all randomly placed ships of computer player.

This project was implemented based on DDD approach and simple event sourcing solution. The main part of design process was to model the domain problem properly. Once the domain model and logic is prepared, we can freely host and run it using any .NET framework.

Altough it does not contain the unit tests so far (it was not implemented using TDD approach), the whole code is fully testable thanks to the strong SOLID principles.

The code implementation is simple but non-trivial. It contains the minimum amount of dependencies which allow to provide working solution which is closed for changes, but opened for extensions.

## Domain model

The main challenge is always to model the domain. It is very important to do it precisely because the infractructure layer will depend on it in the future. It also determines if the software is maintainable easily or not (e.g. exchanging used technologies like database providers, cloud services etc.). 

In case of Battleships the domain is quite simple, so lets analyze it together.

We can describe our domain in couple of points, like following:

* *Battleships* is a **game**
* **Game** consists of two **players** (always! - explained below)
* Every **player** gets his own **game board** and group of **ships**
* Every **ship** needs to be placed on **game board** by **player**
* Once all **players** are ready (have **game boards** with all **ships** placed and prepared), **game session** can be started
* Every **game session** consists of multiple **game session rounds**
* Every **game session round** ends up with **game session round result** which is observed by **game session**
* After every single **game session round**, the next one is started so the opponent **player** can perform an action
* Every **player** performs an action by **saying coordinates** which means he **tries to fire the ship**
* Once the **fire is requested** by **player**, opponent **player** needs to verify his own **game board** and **ships positions** against it
* Once opponent **player** verified the fire attempt, **game session round** ends up with the **game session round result** - can be *missed*, *hit*, *sunk*, *all sunk* or *skipped*
* Once **game session round** is ended up with **all sunk** result, **game session** is over and the winner **player** is announced

It's worth to notice that current business requirement is to prepapre **one-sided** game meanwhile it does not really mean that there is only one player here. That's the **tricky** part.

To prepare the code which is clean and easily extensible without need to change it when business requirements do, we need to define the domain which in case of Battleships clearly says that there are ALWAYS **two players**. We just need to prepare some dummy/mock/placeholder implementations of not needed at this moment parts. Only this way we will be able to quickly pivot between old and new business requirements. That's the power of DDD approach applied using SOLID principles. Adding unit tests in that case are formal - easy and smooth. YAGNI rule is also applied - we do not add not needed features, we just implement the domain which will not change (but can grow, extend) - e.g. there never will be 3 players or more at the same time.

## Application layer

Application layer is as minimalistic as possible. Since *Application* layer in DDD approach is used for "translating" user input to program contract, we also use it for preparing user input to become the Battleships coordinates. We also have here some game components instantiation strategies which allow us to parametrize our game very easily. 

## Hosting

Since domain model is separated abstraction and does not have any dependency to any external code (other than .NET Standard) we can smoothly host it in any .NET Framework or .NET Core environment/framework. The only thing is to provide appropriate UI which will allow for user interaction with our domain model through the domain logic. In other words we can easily host it in:
* Console Applications
* Desktop Applications like WPF or WinForms
* Mobile Applications like Xamarin.Forms or Unity
* Web Applications like REST APIs or WebSockets
* or anything you can imagine

Since the domain model is well designed abstraction layer without any dependencies, we can freely use it in any environment.

## How to run

Just clone the repository, compile it and run the .exe (or just DEBUG). I did not provide executable files on purpose. This should be done by preparing CI/CD pipelines :)