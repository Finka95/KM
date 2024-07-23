import { Typography } from "@mui/material";
import SettingsIcon from '@mui/icons-material/Settings';
import ModeEditIcon from '@mui/icons-material/ModeEdit';
import GenericPage from "./GenericPage";

const ProfilePage = () => {
  return (
    <GenericPage title="Profile" icons={[<SettingsIcon/>, <ModeEditIcon/>]}>
      <Typography>Profile page will be implementd soon</Typography>
    </GenericPage>
  );
};

export default ProfilePage;