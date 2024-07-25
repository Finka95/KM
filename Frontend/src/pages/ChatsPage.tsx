import { Typography } from "@mui/material";
import NotificationsNoneSharpIcon from '@mui/icons-material/NotificationsNoneSharp';
import GenericPage from "./GenericPage";
import { FC } from "react";

const ChatsPage: FC = () => {
  return (
    <GenericPage title="Chats" icons={[<NotificationsNoneSharpIcon/>]}>
      <Typography>Chats page will be implemented soon</Typography>
    </GenericPage>
  );
};

export default ChatsPage;