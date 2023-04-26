MIGRATION RESET COMMANDO


För att ta bort:
Update-database -Migration 0 (tar bort allt innehåll)
remove-migration

Starta om:
add-migration InitialCreate
update-database

Kör ny seed-query i SQL


ADMIN LOGGINS


inlogg är:
tobias@mail.com
hafiz@mail.com
fredrik@mail.com
robin@mail.com
david@mail.com

Lösenord för samtliga är Admin1!


Det är bara med dessa man kan komma åt admin-sidan från layout-view.