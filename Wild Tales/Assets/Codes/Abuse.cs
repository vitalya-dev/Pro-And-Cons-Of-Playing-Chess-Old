using UnityEngine;
using System.Collections;

public class Abuse {
    public static string[] noun = {
        "Kal",
        "Pider",
        "Goamik",
        "Sissska",
        "Rozha",
        "Papik",
        "Zaluupa",
        "Zhopa"
    };

    public static string[] nouns = {
        "Kal",
        "Pidery",
        "Goamiki",
        "Sissski",
        "Rozhie",
        "Papiki",
        "Zaluupi",
        "Zhopi"
    };

    public static string[] adj = {
        "Konchenii",
        "Griaznyjie",
        "Vonychiie",
        "Ebuetcshayi",
        "Huieavi",
        "Dolbaonie",
        "Zarazniye",
        "Falshiviaie"
    };

    public static string make_small_abuse() {
        return noun[Random.Range(0, noun.Length)];
    }

    public static string make_medium_abuse() {
        if (Random.Range(0, 100) < 50)
            return nouns[Random.Range(0, nouns.Length)] + " " + adj[Random.Range(0, adj.Length)];
        else
            return adj[Random.Range(0, adj.Length)] + " " + nouns[Random.Range(0, nouns.Length)];
    }

    public static string make_big_abuse() {
        return nouns[Random.Range(0, nouns.Length)] + " " + adj[Random.Range(0, adj.Length)] + " " + nouns[Random.Range(0, nouns.Length)];
    }
}