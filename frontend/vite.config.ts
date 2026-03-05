import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import mkcert from 'vite-plugin-mkcert';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), mkcert()],
  build: {
    outDir: 'D:/Programación/Visual Studio/repos/movie_catalog/backend/wwwroot',
    emptyOutDir: true,
  },
  server:{
    https: true
  },
  resolve: {
    alias: {
      '@': '/src',
    },
  },
})

