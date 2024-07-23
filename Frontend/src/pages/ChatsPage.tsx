import { Typography } from "@mui/material";
import CircleNotificationsIcon from '@mui/icons-material/CircleNotifications';
import GenericPage from "./GenericPage";
import { FC } from "react";

const ChatsPage: FC = () => {
  return (
    <GenericPage title="Chats" icons={[<CircleNotificationsIcon/>]}>
      <Typography>Chats page will be implemented soon</Typography>
    </GenericPage>
  );
};

export default ChatsPage;