## Introduction

Welcome to LoggiFin! This is my project for a simple finance tracking web app. It uses the .net framework and has currently no split into frontend and backend. This is still a to do for me and will be coming soon. It supports multiple users and uses a mysql database to log the data. Data security is currently not that good. All data is stored as plain text in the DB. This is a project for me to learn a diffrent web framework and develope some software I really want to use. So strap in for the developement ride. Its gonna be a wild one :D.

This is one of my earlier projects and is currently being reworked with agentic technologies. More to come.

## Local database configuration

Database connections are loaded from required environment variables:

- `LOGGIFIN_TEST_DB_CONNECTION_STRING`
- `LOGGIFIN_PROD_DB_CONNECTION_STRING`

Example placeholder values (replace with your own local credentials):

```bash
export LOGGIFIN_TEST_DB_CONNECTION_STRING="server=localhost;uid=YOUR_TEST_USER;******;database=loggifinTest"
export LOGGIFIN_PROD_DB_CONNECTION_STRING="server=localhost;uid=YOUR_PROD_USER;******;database=loggifin"
```
