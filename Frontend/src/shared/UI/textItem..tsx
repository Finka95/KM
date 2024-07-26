import { Grid, Typography } from "@mui/material";
import { FC } from "react";

type TextItemProps = {
  title: string,
  content: string | number,
  noEllipsis?: boolean
}

const TextItem: FC<TextItemProps> = ({ title, content, noEllipsis = false }) => {
  return (
    <Grid item xs={12}>
      <Typography 
        variant="h6" 
        sx={{ mb: 1, fontWeight: 'normal', color: 'rgb(113, 113, 113)', fontSize: "16px" }}
      >
        {title}?
      </Typography>
      <Typography 
        variant="h6"
        sx={{
          overflow: 'hidden',
          textOverflow: noEllipsis ? 'unset' : 'ellipsis',
          whiteSpace: noEllipsis ? 'normal' : 'nowrap',
          wordBreak: 'break-word',
          maxHeight: noEllipsis ? 'unset' : '1.5rem',
          color: "rgb(18, 18, 18)",
          fontWeight: "600"
        }}
      >
        {content}
      </Typography>
    </Grid>
  );
};

export default TextItem;