import { Typography } from "@mui/material";
import TuneIcon from '@mui/icons-material/Tune';
import GenericPage from "./GenericPage";

const HitOrMissPage = () => {
  return (
    <GenericPage title="Hit or miss" icons={[<TuneIcon/>]}>
      <Typography>Hit or miss page will be implemented soon</Typography>
    </GenericPage>
  )
}

export default HitOrMissPage;
