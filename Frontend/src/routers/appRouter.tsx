import { Route, Routes } from "react-router-dom"
import { privateRoutes } from "./routesList"

const AppRouter = () => {
  const routes = privateRoutes.map(route =>
    <Route key={route.title}
        path={route.path}
        element={route.element}
    />
)

  return (
    <Routes>{routes}</Routes>
  )
}

export default AppRouter