from fastapi import FastAPI, Request
from fastapi.responses import HTMLResponse, JSONResponse
import httpx
import uvicorn
import requests
import os

from services import Services

app = FastAPI(redirect_slashes=False)

@app.get("/health")
def index():
    return {"proxy status": "ok"}

@app.exception_handler(ValueError)
async def value_error_exception_handler(request: Request, exc: ValueError):
    return JSONResponse(
        status_code=400,
        content={"message": str(exc)},
    )

@app.get("/api/{microservice}")
@app.get("/api/{microservice}/health")
def proxy_get(request: Request, microservice: str):
    if microservice in Services:
        migration_toggle = Services[microservice]["migration"]
        host = Services[microservice]["host"]
        route = Services[microservice]["route"]
        
        port = "8080" if migration_toggle == False else Services[microservice]["port"]
        url = f"http://{host}:{port}/{route}"

        result = requests.get(url = url)
        
        return result.json()
 
@app.post("/api/{microservice}")
def proxy_get(request: Request, microservice: str):
    if microservice in Services:
        migration_toggle = Services[microservice]["migration"]
        host = Services[microservice]["host"]
        route = Services[microservice]["route"]
        
        port = "8080" if migration_toggle == False else Services[microservice]["port"]
        url = f"http://{host}:{port}/{route}"

        result = requests.get(url = url)
        
        return result.json()
