﻿using System;
using Login;
using Menu;
using System.Runtime.CompilerServices;
class Precence
{
    static void Main(string[] args)
    {

        List<Student> students = new List<Student>(){
        };
        List<Student> test = new List<Student>();
        students.Add(new Student("CHABEAUDIE" , "Maxime"));
        students.Add(new Student("FETTER" , "Léo"));
        students.Add(new Student("MONTASTIER" , " Florian"));
        students.Add(new Student("MARTIN" , "Jérémy"));
        students.Add(new Student("OHIN" , "ELvis"));


        //Créer un tableau d'absents vide de type integer
        List<Student> absents = new List<Student>();
        //Faire une boucle tant que l'utilisateur n'a pas tapé "fin"
        Console.WriteLine(@$"
        _____       _     _            _                         __
        / ____|     (_)   (_)          | |                       /_/
        | (___   __ _ _ ___ _  ___    __| | ___  ___   _ __  _ __ ___  ___  ___ _ __   ___ ___  ___
        \___ \ / _` | / __| |/ _ \  / _` |/ _ \/ __| | '_ \| '__/ _ \/ __|/ _ \ '_ \ / __/ _ \/ __|
        ____) | (_| | \__ \ |  __/ | (_| |  __/\__ \ | |_) | | |  __/\__ \  __/ | | | (_|  __/\__ \
        |_____/ \__,_|_|___/_|\___|  \__,_|\___||___/ | .__/|_|  \___||___/\___|_| |_|\___\___||___/
                                                    | |
                                                    |_|
        ");
        Console.WriteLine("---------------------------------------------------------");
        Login.Controller.Login.Auth();
         //Création de la liste des étudiants
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Menu.Controller.Code.MainMenu();
        }
    }
}










