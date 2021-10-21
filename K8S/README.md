## Ingress Nginx

Follow [this tutorial](https://kubernetes.github.io/ingress-nginx/deploy/#docker-desktop) to install `Ingress`

If you use Docker Desktop with Kubernetes, specify your host ( in this case kubernetes.local ) to 127.0.0.1

## Create secret for mssql

`kubectl create secret generic mssql --from-literal=SA_PASSWORD="your-pass"`