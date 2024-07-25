import { Typography } from "@mui/material";
import TuneIcon from '@mui/icons-material/Tune';
import GenericPage from "./GenericPage";
import { FC } from "react";

const NearPage: FC = () => {
  return (
    <GenericPage title="Near" icons={[<TuneIcon/>]}>
      <Typography>Near page will be implemented soon</Typography>
    </GenericPage>
  );
};

export default NearPage;