# Как handle errors на фронте?

Сейчас система выстроена таким образом, что 
можно просто делать throw new *Exception()* и всё.
CustomErrorBoundary будет автоматически их 
отлавливать и выводить на экран в виде Toast с 
ToastLevel.Error.