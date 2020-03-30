using UnityEngine;
using System.Collections;

public class Abuse {
    public static string[] nouns = {
        "Kal",
        "Otrebya",
        "Ubliudki",
        "Pidery",
        "Osloyoabi",
        "Vieblyadky",
        "Goamiki",
        "Sissski",
        "Rozhie",
        "Papiki",
        "Vyrodki",
        "Zaluupi",
        "Zhopi"
    };

    public static string[] adj = {
        "Konchenii",
        "Ebyanii",
        "Griaznyjie",
        "Vonychiie",
        "Ebuetcshayi",
        "Huieavi",
        "Dolbaonie",
        "Zarazniye",
        "Falshiviaie"
    };

    public static string make_small_abuse() {
        if (Random.Range(0, 100) < 50)
            return nouns[Random.Range(0, nouns.Length)] + " " + adj[Random.Range(0, adj.Length)];
        else
            return adj[Random.Range(0, adj.Length)] + " " + nouns[Random.Range(0, nouns.Length)];
    }
}