﻿using System;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        Config.DatabaseInitializer.Initialize();

        // Affichez le titre de l'application en utilisant l'art ASCII
        AnsiConsole.Write(
            new FigletText("NewCall")
            .LeftJustified()
            .Color(Color.Blue));

        // Affichez une ligne horizontale pour séparer visuellement le titre du reste
        var rule = new Rule("[blue]Authentification[/]");
        rule.Justification = Justify.Left;
        AnsiConsole.Write(rule);
        // Gérez la connexion de l'utilisateur
        Controller.LoginController.ValidationLogin();

        // Effacez l'écran pour une meilleure expérience utilisateur
        Console.Clear();

        // Affichez le menu principal tant que l'utilisateur ne choisit pas de quitter
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Controller.MenuController.MainMenu();
        }
    }
}
