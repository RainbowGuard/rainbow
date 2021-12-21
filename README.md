# rainbow

## Database setup
```
create database rainbow;
create user 'rainbow'@localhost identified by 'rainbow';
grant all permissions on 'rainbow'.* to 'rainbow';
flush privileges;
```