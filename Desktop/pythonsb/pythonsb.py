import http.client

conn = http.client.HTTPSConnection("nbgnodeapi.cloudapp.net")

headers = { 'accept': "applicatio/json" }

conn.request("GET", "/api/banks", headers=headers)

res = conn.getresponse()
data = res.read()

print(data.decode("utf-8"))
