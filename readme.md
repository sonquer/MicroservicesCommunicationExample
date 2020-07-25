# Microservices communication example

Synchronous communication using RabbitMQ

---

#### Docker

`docker-compose -f docker-compose.yml -f docker-compose.override.yml build`
`docker-compose up`

#### Services

- [x] users.api
- [x] products.api
- [x] gateway.api (port: 5000)

#### Infrastructure

- [x] rabbitMq (ports: 15672, 5672)