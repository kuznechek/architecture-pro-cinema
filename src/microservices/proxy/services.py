import json

# class MicroserviceModel:

#     def __init__(self, route, host, port):
#         self.route = route
#         self.host = host
#         self.port = port

# Services = {
#         "movies": {
#             "route": "/api/movies",
#             "host": "microservice-movies",
#             "port": "8081"
#             },
#         "users": {
#             "route": "/api/users",
#             "host": "microservice-movies",
#             "port": "8081"
#             }
        
# }

Services = {}
with open("services.json", "rb") as file:
    Services = json.load(file)