# Artist Management - Project .NET Framework

* Naam: Xander Geluykens
* Studentennummer: 0160916-90
* Academiejaar: 24-25
* Klasgroep: INF201
* Onderwerp: Artist * - * Artwork * - 1 Museum

## Sprint 1
 ```mermaid
 classDiagram
 class Artist
 class Artwork
 class Museum
 
 Artist "*" -- "*" Artwork
 Artwork "*" -- "1" Museum
 ```

## Sprint 3

### Beide zoekcriteria ingevuld
```sqlite
SELECT "a"."ArtistId", "a"."Age", "a"."BirthDate", "a"."DeathDate", "a"."FirstName", "a"."LastName"
FROM "Artists" AS "a"
WHERE instr("a"."LastName", @__name_0) > 0 AND "a"."BirthDate" = @__birthday_1
```

### Enkel zoeken op naam
```sqlite
SELECT "a"."ArtistId", "a"."Age", "a"."BirthDate", "a"."DeathDate", "a"."FirstName", "a"."LastName"
FROM "Artists" AS "a"
WHERE instr("a"."LastName", @__name_0) > 0
```

### Enkel zoeken op geboortedatum
```sqlite
SELECT "a"."ArtistId", "a"."Age", "a"."BirthDate", "a"."DeathDate", "a"."FirstName", "a"."LastName"
FROM "Artists" AS "a"
WHERE "a"."BirthDate" = @__birthday_0
```

### Beide zoekcriteria leeg
```sqlite
SELECT "a"."ArtistId", "a"."Age", "a"."BirthDate", "a"."DeathDate", "a"."FirstName", "a"."LastName"
FROM "Artists" AS "a"
```

## Sprint 4
 ```mermaid
 classDiagram
 class Artist
 class ArtistArtwork
 class Artwork
 class Museum
 
 Artist "*" -- "1" ArtistArtwork
 ArtistArtwork "1" -- "*" Artwork
 Artwork "*" -- "1" Museum
 ```

## Sprint 6
### Nieuw Museum
#### Request
```http request
POST http://localhost:5066/api/museums
Content-Type: application/json

{
  "name": "Belgisch Stripcentrum - Museum",
  "location": "Brussels",
  "yearEstablished": "1989"
}
```

#### Response
```http request
HTTP/1.1 201 Created
Content-Type: application/json; charset=utf-8
Date: Fri, 20 Dec 2024 08:00:50 GMT
Server: Kestrel
Location: http://localhost:5066/api/museums/5
Transfer-Encoding: chunked

{
  "museumId": 5,
  "name": "Belgisch Stripcentrum - Museum",
  "location": "Brussels",
  "yearEstablished": 1989,
  "artworks": null
}
```

## Sprint 7
### Gebruikers
* xander@kdg.be - Password1! - User
* jasper@kdg.be - Password1! - User
* sam@kdg.be - Password1! - User
* quinten@kdg.be - Password1! - User
* admin@kdg.be - Password1! - Admin

### Nieuw Museum niet ingelogd
#### Request
```http request
POST http://localhost:5066/api/museums
Content-Type: application/json

{
  "name": "Belgisch Stripcentrum - Museum",
  "location": "Brussels",
  "yearEstablished": "1989"
}
```

#### Response
```http request
HTTP/1.1 401 Unauthorized
Content-Length: 0
Date: Thu, 27 Feb 2025 11:00:12 GMT
Server: Kestrel
Location: http://localhost:5066/Identity/Account/Login?ReturnUrl=%2Fapi%2Fmuseums
```

### Nieuw Museum ingelogd
#### Request
```http request
POST http://localhost:5066/api/museums
Content-Type: application/json
Cookie: .AspNetCore.Antiforgery.YJng0G0HcEg=CfDJ8Ni6NbqycndBm5AxGViVgXm1FZh1eBCR6jn4vE-We2D0BsQjBGvDyEf3snW1xdvPvX2bt_izldYdFi-hcTd5yu9FmD3ooEDDcHIl6iis66EEXeUMYLIVyB91OPIUumA6sRgYmKaXBkJbowIpqusyqS4
Cookie: .AspNetCore.Identity.Application=CfDJ8Ni6NbqycndBm5AxGViVgXm-wFEbY0KlSy0ossyZ1nCGd7892qLovGCaP7p3tJCKCS5KliAHPZ3g2MdVFdakXbUH_Nw7-XprSSQN7PSw5gH4UKLocx5V8U5Hybsc3OEHpzJm0XaEN3Co2yrAgaAxn3ZKbfnnIM2iLODbp7yOtKZkySVvOunpTDSb80DBf_Z9DBTpzFC_vHF2ezaTXfUj95W6ofu0ttw08saAeUZTdivj-jt9OECMYO2_2Ym3eeSupw5imsQoC6ZxOy388VhHYOXrMaklegjUwmQ-io1QV2nFSscrxeNans-8SpvI9NrmsuL1J-iin2EDF5uzrnUxUOYi_2s9rRAnHBef7ZUPF945V0PjbNZdyyhIDRndyqiEaKkP5-Q8wd_oVo_iE8_NGSkK5469-bsBHvvyA9eiip2akMZVIigwUBi54yUHWVKdGjV-2926F05h1cB5H8kBvGX1PiaMhNnRBzIoD3SZc4Tw8AgGELOjwcy_Eok7IUQUNaTE0VswgtH3vEOe_yiEuQrEp7lydTxZWQVpRFHQDhA61W3SNvrVht0CFGiaLXsiV4e8zJcfdchMo9yadNhFRggQG5IolX_xxAJHfAiG_lLxvIcY8fWorzlK8UYoSis9mXCsMweqo-FxrgPrD2Yy5i1luHewZArQXlXHVVlMMgxUaZemsKMuxpdoipwk1GLKJYBFybZHh7A1SZqvhNMQ7dIGYozWjKr1tPlG8RYGKQ51IIJSplFmEgAJxkxfLTNZFdHPLd76poVGCizynU47JkmTVxCaJaOdR3RM-k80591aLke3T-XGpG9CTsat5MMLAZRADlDaDstT9j34oRzc20U

{
  "name": "Belgisch Stripcentrum - Museum",
  "location": "Brussels",
  "yearEstablished": "1989"
}
```

#### Response
```http request
HTTP/1.1 201 Created
Content-Type: application/json; charset=utf-8
Date: Thu, 27 Feb 2025 11:01:14 GMT
Server: Kestrel
Location: http://localhost:5066/api/museums/5
Transfer-Encoding: chunked

{
  "museumId": 5,
  "name": "Belgisch Stripcentrum - Museum",
  "location": "Brussels",
  "yearEstablished": 1989,
  "artworks": null
}
```

## Sprint 8
```shell 
dotnet test 
``` 

![Code coverage](img_1.png)

```
Complexe authorization is getest op ArtworkControllerTests.cs

Verification is getest op ManagerTests.cs
``` 

['Tests' tabblad](https://gitlab.com/kdg-ti/programmeren-2-dotnet/24-25/inf201/projects/xander.geluykens/-/pipelines/1967311830/test_report?job_name=tests)

[Code coverage Raport](https://kdg-ti.gitlab.io/-/programmeren-2-dotnet/24-25/inf201/projects/xander.geluykens/-/jobs/10917632726/artifacts/coveragereport/index.html)