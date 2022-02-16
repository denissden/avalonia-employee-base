#! /bin/bash

docker run --rm --name pgdocker -e POSTGRES_PASSWORD=password -e POSTGRES_USER=user -e POSTGRES_DB=avalonia_sem6pr1 -d -p 5432:5432 -v $HOME/docker/volumes/postgres_avalonia:/var/lib/postgresql/data postgres;
