import { Typography } from "@mui/material";
import SettingsOutlinedIcon from '@mui/icons-material/SettingsOutlined';
import CreateOutlinedIcon from '@mui/icons-material/CreateOutlined';
import GenericPage from "./GenericPage";
import { FC } from "react";

const ProfilePage: FC = () => {
  return (
    <GenericPage title="Profile" icons={[<SettingsOutlinedIcon/>, <CreateOutlinedIcon/>]}>
      <Typography>Profile page will be implementd soon</Typography>
    </GenericPage>
  );
};

export default ProfilePage;