import { Typography } from "@mui/material";
import TuneIcon from '@mui/icons-material/Tune';
import SubscriptionsIcon from '@mui/icons-material/Subscriptions';
import GenericPage from "./GenericPage";

const NearPage = () => {
  return (
    <GenericPage title="Near" icons={[<SubscriptionsIcon/>, <TuneIcon/>]}>
      <Typography>Near page will be implemented soon</Typography>
    </GenericPage>
  );
};

export default NearPage;