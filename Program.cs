﻿using System;
using Login;
using System.Runtime.CompilerServices;
class Precence
{
    static void Main(string[] args)
    {
        //Création de la liste des étudiants

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
        String? s;
        //Afficher la liste des étudiants à chaque étudiant.
        foreach (Student studentCurrent in students) {
            do {
                Console.WriteLine($"L'étudiant {studentCurrent.LastName} {studentCurrent.Firstname} est-il absent ou présent ? Tapez 'a' pour absent ou 'p' pour présent");

                //Lire ce que l'utilisateur a tapé
                s = Console.ReadLine();
                switch (s)   {
                    //Ajouter l'étudiant à la liste des absents
                    case "a" or "A":
                        absents.Add(studentCurrent);
                        s = "ok";
                        Console.WriteLine($"Absent");
                        break;

                    case "p" or "P":
                        s = "ok";
                        Console.WriteLine($"Présent");
                        break;
                    //Si c'est incorrect, afficher message d'erreur
                    default:
                        Console.WriteLine($"Erreur. Taper 'a' ou 'p'");
                        break;
                    }
                } while (s != "ok");

            }
        Console.WriteLine("---------------------------------------------------------");
        //Afficher la liste des absents
        Console.WriteLine($"Liste des absents");
        foreach (Student currentAbsents in absents) {
            Console.WriteLine($"{currentAbsents.LastName}  {currentAbsents.Firstname}");
        }
        do {
            Console.WriteLine($"Appuyer sur Enter pour quitter");
            s = Console.ReadLine();
            switch (s)   {
                default:
                    break;
            }
        } while (s == null);
    }
}










