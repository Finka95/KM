import { BrowserRouter } from "react-router-dom";
import AppRouter from "./routers/appRouter";

function App() {
  return (
    <BrowserRouter>
      <AppRouter/>
    </BrowserRouter>
  )
}

export default App
