import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      "/change-tickets": {
        target: "http://localhost:5067",
        changeOrigin: true
      },
      "/health": {
        target: "http://localhost:5067",
        changeOrigin: true
      }
    }
  }
});
