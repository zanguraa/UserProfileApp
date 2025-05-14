FROM node:18-alpine

WORKDIR /app

# Copy app files
COPY . .

# Install dependencies
RUN npm install

# Expose CRA port
EXPOSE 3000

# Start app
CMD ["npm", "start"]
