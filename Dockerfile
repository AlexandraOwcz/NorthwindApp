FROM mongo 

COPY ./data/ /northwind-data/

ENTRYPOINT ["docker-entrypoint.sh"]