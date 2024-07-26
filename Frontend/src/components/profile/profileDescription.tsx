import { Box, Card, CardContent, Grid } from "@mui/material";
import TextItem from "../../shared/UI/textItem.";
import { FC } from "react";
import { Gender } from "../../shared/enums/gender";

type ProfileDescriptionProps = {
  description: string,
  city: string,
  age: number,
  email: string,
  gender: Gender
} 

const ProfileDescription: FC<ProfileDescriptionProps> = ({ description, city, age, email, gender }) => {
  return (
    <Box sx={{ mb: 3 }}>
      <Card variant="outlined">
        <CardContent>
          <Grid container spacing={2}>
            <TextItem title="Some things people should know about you" content={description} noEllipsis />
            <TextItem title="Where do you live?" content={city} />
            <TextItem title="How old are you?" content={age} />
            <TextItem title="What is yout email" content={email} />
            <TextItem title="What is your gender" content={gender} />
          </Grid>
        </CardContent>
      </Card>
    </Box>
  );
};

export default ProfileDescription;