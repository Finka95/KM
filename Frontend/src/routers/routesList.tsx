import LikesPage from '../pages/LikesPage';
import ChatsPage from '../pages/ChatsPage';
import NearPage from "../pages/NearPage";
import ProfilePage from "../pages/ProfilePage";
import NotFoundPage from "../pages/NotFoundPage";
import HitOrMissPage from "../pages/HitOrMissPage";

interface RouterType {
  title: string;
  path: string;
  element: JSX.Element;
}

export const privateRoutes: RouterType[] = [
  { title: "Likes", path: '/likes', element: <LikesPage /> },
  { title: "Chats", path: '/chats', element: <ChatsPage /> },
  { title: "Home", path: '/near', element: <NearPage /> },
  { title: "HitOrMiss", path: "/", element: <HitOrMissPage /> },
  { title: "Profile", path: "/profile", element: <ProfilePage /> },
  { title: "NotFound", path: "*", element: <NotFoundPage /> },
]