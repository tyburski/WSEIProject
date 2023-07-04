# WSEIProject - InstaClone - Aplikacja do udostępniania zdjęć

Projekt InstaClone to społecznościowa aplikacja pozwalająca użytkownikom na dzielenie się zdjęciami. Inspirując się popularnymi platformami społecznościowymi, InstaClone oferuje bogatą funkcjonalność do dzielenia się i odkrywania treści od innych użytkowników.

## Funkcje

InstaClone dostarcza wiele interesujących funkcji:

- Dodawanie i usuwanie użytkowników.
- Zmiana hasła użytkownika.
- Dodawanie i udostępnianie zdjęć.
- Użytkownicy mogą dodawać swoje opinie za pomocą komentarzy i polubień.

## Instalacja

Aby zainstalować InstaClone na swoim urządzeniu, wykonaj poniższe kroki:

1. Należy sklonować repozytorium.
2. Następnie otworzyć je za pomocą Visual Studio.
3. Następnie dokonać migracji za pomocą Entity Framework.
4. Uruchomić projekt.

## Przykład użycia

Załóżmy, że jesteś entuzjastą fotografii i chciałbyś podzielić się swoimi najnowszymi zdjęciami ze społecznością. Po zainstalowaniu aplikacji InstaClone, załóż swoje konto, dodaj kilka interesujących zdjęć.

Przykładowe Endpointy do zrealizowania konkretnych działań:

Dodawanie użytkownika:
- [POST] (localhost) /api/User/create
Logowanie:
- [POST] (localhost) /api/User/login
Dodawanie uprawnień użytkownika:
- [POST] (localhost) /api/User/permissions/{id}
Zmiana hasła:
- [PUT] (localhost) /api/User/newpassword
Usunięcie konta:
- [DELETE] (localhost) /api/User/delete/{id}

Dodawanie zdjęcia:
- [POST] (localhost) /api/Photo/add
Wyświetlanie zdjęć:
- [GET] (localhost) /api/Photo/photos
Wyświetlanie konkretnego zdjęcia:
- [GET] (localhost) /api/Photo/photo/{id}
Wyświetlanie zdjęć użytkownika:
- [GET] (localhost) /api/Photo/photos/{username}
Usunięcie zdjęcia:
- [DELETE] (localhost) /api/Photo/photo/delete/{id}
Dodawanie komentarza:
- [POST] (localhost) /api/Photo/photo/{id}/createcomment
Polubienie zdjęcia:
- [POST] (localhost) /api/Photo/photo/{id}/like

Polubienie komentarza:
- [POST] (localhost) /api/Comment/comment/{id}/like
Usunięcie komentarza:
- [POST] (localhost) /api/Comment/comment/{id}/delete

## Status projektu

W projekcie zostało wykonane API umożliwiające wykonywanie wszystkich funkcjonalności opisanych powyżej.

## Autorzy

- Dawid Tyburki (dawid.tyburksi@microsoft.wsei.edu.pl)
- Kacper Torba (kacper.torba@microsoft.wsei.edu.pl)
